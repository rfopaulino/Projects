import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from './authentication/authentication.service';
import { API } from './app.api'
 
@Injectable()
export class AppService {

  constructor(private http: HttpClient, private authenticationService: AuthenticationService) { }

  // Usuário
  UsuarioGrid(): Observable<any> {
    return this.http.get(`${API}/usuario/grid`);
  }
  UsuarioGet(id): Observable<any> {
    return this.http.get(`${API}/usuario/` + id);
  }
  UsuarioPost(dto): Observable<any> {
    return this.http.post<any>(`${API}/usuario`, dto);
  }
  UsuarioPut(id, dto): Observable<any> {
    return this.http.put<any>(`${API}/usuario/` + id, dto);
  }
  UsuarioDelete(dto: string[]): Observable<any> {
    return this.http.post<any>(`${API}/usuario/deletar`, dto);
  }
  UsuarioInativar(dto: string[]): Observable<any> {
    return this.http.post<any>(`${API}/usuario/inativar`, dto);
  }

  // Médico
  MedicoGrid(): Observable<any> {
    return this.http.get(`${API}/medico/grid`);
  }
  MedicoGet(id): Observable<any> {
    return this.http.get(`${API}/medico/` + id);
  }
  MedicoPost(dto): Observable<any> {
    return this.http.post(`${API}/medico`, dto);
  }
  MedicoPut(id, dto): Observable<any> {
    return this.http.put<any>(`${API}/medico/` + id, dto);
  }
  MedicoDelete(dto: string[]): Observable<any> {
    return this.http.post<any>(`${API}/medico/deletar`, dto);
  }

  // Paciente
  PacienteGrid(): Observable<any> {
    return this.http.get(`${API}/paciente/grid`);
  }
  PacienteGet(id): Observable<any> {
    return this.http.get(`${API}/paciente/` + id);
  }
  PacientePost(dto): Observable<any> {
    return this.http.post(`${API}/paciente`, dto);
  }
  PacientePut(id, dto): Observable<any> {
    return this.http.put<any>(`${API}/paciente/` + id, dto);
  }
  PacienteDelete(dto: string[]): Observable<any> {
    return this.http.post<any>(`${API}/paciente/deletar`, dto);
  }

  // Agendamento
  AgendamentoGrid(): Observable<any> {
    return this.http.get(`${API}/agendamento/grid`);
  }
  AgendamentoGet(id): Observable<any> {
    return this.http.get(`${API}/agendamento/` + id);
  }
  AgendamentoPost(dto): Observable<any> {
    return this.http.post(`${API}/agendamento`, dto);
  }
  AgendamentoPut(id, dto): Observable<any> {
    return this.http.put<any>(`${API}/agendamento/` + id, dto);
  }
  AgendamentoDelete(dto: string[]): Observable<any> {
    return this.http.post<any>(`${API}/agendamento/deletar`, dto);
  }
}
