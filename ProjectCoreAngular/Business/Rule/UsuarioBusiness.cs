using Business.Dto.Usuario;
using Common;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Business.Rule
{
    public class UsuarioBusiness
    {
        private readonly UnitOfWork _uow;

        public UsuarioBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool ExistsUsuario(string usuario)
        {
            return _uow.UsuarioRepository.GetById(usuario) != null;
        }

        private void ExcluirUsuarios(List<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                _uow.UsuarioRepository.Delete(usuario);
            }
            _uow.SaveChanges();
        }

        private void InativarUsuarios(List<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                usuario.Ativo = false;
                _uow.UsuarioRepository.Edit(usuario);
            }
            _uow.SaveChanges();
        }

        public UsuarioGetIdDto GetById(string id)
        {
            var db = _uow.UsuarioRepository.GetById(id);
            if(db  != null)
            {
                return new UsuarioGetIdDto
                {
                    Id = db.UsuarioLogin,
                    Usuario = db.UsuarioLogin,
                    Senha = db.Senha,
                    Nome = db.Nome,
                    Sobrenome = db.Sobrenome,
                    Sexo = db.Sexo,
                    Rg = db.Rg,
                    Cpf = db.Cpf,
                    Cep = db.Cep,
                    Logradouro = db.Logradouro,
                    Bairro = db.Bairro,
                    Numero = db.Numero,
                    Nacionalidade = db.Nacionalidade,
                    Telefone = db.Telefone,
                    Celular = db.Celular
                };
            }
            else
                throw new Exception(Messages.NotExistsUser);
        }

        public UsuarioAuthenticationDto Authentication(UsuarioLoginDto dto, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            var usuario = GetById(dto.Usuario);

            CryptographyHelper cryptHelper = new CryptographyHelper();
            bool authenticated = cryptHelper.VerifyPassword(dto.Senha, usuario.Senha);

            if (authenticated)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Usuario, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Usuario)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new UsuarioAuthenticationDto
                {
                    authenticated = true,
                    message = "OK",
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    usuario = usuario.Usuario,
                };
            }
            else
                throw new Exception("Falha ao autenticar");
        }

        public Usuario Insert(UsuarioInsertDto dto)
        {
            if (!ExistsUsuario(dto.Usuario))
            {
                ValidateDocumentHelper.ValidateCpf(dto.Cpf);

                CryptographyHelper cryptHelper = new CryptographyHelper();
                dto.Senha = cryptHelper.EncryptPassword(dto.Senha);

                var db = new Usuario
                {
                    UsuarioLogin = dto.Usuario,
                    Senha = dto.Senha,
                    Nome = dto.Nome,
                    Sobrenome = dto.Sobrenome,
                    Sexo = dto.Sexo,
                    Rg = dto.Rg,
                    Cpf = dto.Cpf,
                    Cep = dto.Cep,
                    Logradouro = dto.Logradouro,
                    Bairro = dto.Bairro,
                    Numero = dto.Numero,
                    Nacionalidade = dto.Nacionalidade,
                    Telefone = dto.Telefone,
                    Celular = dto.Celular
                };

                _uow.UsuarioRepository.Add(db);
                _uow.SaveChanges();

                return db;
            }
            else
                throw new Exception(Messages.ExistsUser);
        }

        public void Update(string id, UsuarioUpdateDto dto)
        {
            if (id != dto.Id)
                throw new Exception(Messages.InconsistencyRequest);

            ValidateDocumentHelper.ValidateCpf(dto.Cpf);

            if (ExistsUsuario(id))
            {
                var db = _uow.UsuarioRepository.GetById(id);

                db.Nome = dto.Nome;
                db.Sobrenome = dto.Sobrenome;
                db.Sexo = dto.Sexo;
                db.Rg = dto.Rg;
                db.Cpf = dto.Cpf;
                db.Cep = dto.Cep;
                db.Logradouro = dto.Logradouro;
                db.Bairro = dto.Bairro;
                db.Numero = dto.Numero;
                db.Nacionalidade = dto.Nacionalidade;
                db.Telefone = dto.Telefone;
                db.Celular = dto.Celular;

                _uow.UsuarioRepository.Edit(db);
                _uow.SaveChanges();
            }
            else
                throw new Exception(Messages.NotExistsUser);
        }

        public List<UsuarioGridDto> Grid()
        {
            var query = _uow.UsuarioRepository.GetAll();
            return query
                .Where(x => x.Ativo)
                .Select(x => new UsuarioGridDto
                {
                    Id = x.UsuarioLogin,
                    Nome = x.Nome.Trim(),
                    Sobrenome = x.Sobrenome.Trim(),
                    Usuario = x.UsuarioLogin.Trim()
                }).ToList();
        }

        public void ExcluirSelecionados(List<string> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new Exception(Messages.InconsistencyRequest);

            var usuarios = _uow.UsuarioRepository.GetByIds(ids);
            ExcluirUsuarios(usuarios);
        }

        public void InativarSelecionados(List<string> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new Exception(Messages.InconsistencyRequest);

            var usuarios = _uow.UsuarioRepository.GetByIds(ids);
            InativarUsuarios(usuarios);
        }
    }
}
