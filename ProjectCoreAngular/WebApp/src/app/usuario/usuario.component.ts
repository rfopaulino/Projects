import { Component, OnInit } from "@angular/core"
import { AppService } from "../app.service";
import { GridOptions } from "ag-grid";
import { AuthenticationService } from "../authentication/authentication.service";
import { ErrorService } from "../error.service";
import { NgForm } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import { AlertComponent } from "../alert/alert.component";
import { AlertService } from "../alert.service";
import { AgGridClickEditComponent } from "../ag-grid.click.edit.component";
import { RG, CPF, CEP, TELEFONE, CELULAR } from '../app.mask'

class ModelUsuario {
  Id: string;
  Nome: string;
  Sobrenome: string;
  Sexo: number;
  Usuario: string;
  Senha: string;
  Rg: string;
  Cpf: string;
  Cep: string;
  Logradouro: string;
  Bairro: string;
  Numero: number;
  Nacionalidade: string;
  Telefone: string;
  Celular: string;
}

@Component({
  selector: 'mt-usuario',
  templateUrl: 'usuario.component.html',
  styleUrls: ['usuario.component.css'],
  providers: [ModelUsuario]
})
export class UsuarioComponent implements OnInit {

  public gridOptions: GridOptions;
  public rowData: any[];
  public columnDefs: any[];

  Mask: {
    Rg: Array<string | RegExp>,
    Cpf: Array<string | RegExp>,
    Cep: Array<string | RegExp>,
    Telefone: Array<string | RegExp>,
    Celular: Array<string | RegExp>
  };

  MaisAcoes: number;

  openAlertIn$: Observable<boolean>;

  constructor(private model: ModelUsuario, private appService: AppService, private authenticationService: AuthenticationService, private errorService: ErrorService, private alertService: AlertService) {

    this.Mask = {
      Rg: RG,
      Cpf: CPF,
      Cep: CEP,
      Telefone: TELEFONE,
      Celular: CELULAR
    }

    this.MaisAcoes = -1;

    this.gridOptions = <GridOptions>{
      onGridReady: () => {
        this.gridOptions.api.sizeColumnsToFit();
      },
      context: {
        componentParent: this
      }
    };
    this.columnDefs = [
      {
        headerName: '',
        width: 20,
        checkboxSelection: true,
        suppressSorting: true,
        suppressMenu: true,
        pinned: true
      },
      { headerName: "Nome", field: "Nome" },
      { headerName: "Sobrenome", field: "Sobrenome" },
      { headerName: "Usuário", field: "Usuario" },
      {
        headerName: "Editar",
        field: "value",
        cellRendererFramework: AgGridClickEditComponent,
        colId: "params",
        width: 30
      }
    ];
    this.rowData = [];
  }

  methodFromParent(cell) {
    let id: number = this.gridOptions.rowData[cell].Id;
    this.appService.UsuarioGet(id)
      .subscribe(
      response => {
        this.model = response;
        },
        error => this.errorService.commonCatch(error.status));
  }

  selectAllRows() {
    this.gridOptions.api.selectAll();
  }

  ngOnInit() {
    this.clearDto();
    this.grid();
    this.openAlertIn$ = this.authenticationService.isLoggedIn;
  }

  grid() {
    this.appService.UsuarioGrid()
      .subscribe(
      response => {
        this.rowData = response;
      },
      error => {
        this.errorService.commonCatch(error.status);
      });
  }

  save(myForm: NgForm) {
    if (myForm.form.valid) {
      if (!this.model.Id) {
        this.appService.UsuarioPost(this.model)
          .subscribe(
            response => {
              this.alertService.success();
              this.clearDto();
              this.grid();
              myForm.form.markAsPristine();
            },
            error => this.errorService.commonCatch(error.status));
      }
      else {
        this.appService.UsuarioPut(this.model.Id, this.model)
          .subscribe(
            response => {
              this.alertService.success();
              this.clearDto();
              this.grid();
              myForm.form.markAsPristine();
            },
            error => this.errorService.commonCatch(error.status));
      }
    }
    else
      this.alertService.warning();
  }

  cancel() {
    this.clearDto();
  }

  clearDto() {
    this.model.Id = undefined;
    this.model.Nome = undefined;
    this.model.Sobrenome = undefined;
    this.model.Sexo = -1;
    this.model.Usuario = undefined;
    this.model.Senha = undefined;
    this.model.Rg = undefined;
    this.model.Cpf = undefined;
    this.model.Cep = undefined;
    this.model.Logradouro = undefined;
    this.model.Bairro = undefined;
    this.model.Numero = undefined;
    this.model.Nacionalidade = "";
    this.model.Telefone = undefined;
    this.model.Celular = undefined;
  }

  excluir() {
    let ids: string[] = this.gridOptions.api.getSelectedRows().map(item => item.Id);
    if (ids.length > 0) {
      this.appService.UsuarioDelete(ids)
        .subscribe(
          response => {
            this.alertService.success();
            this.grid();
          },
          error => this.errorService.commonCatch(error.status));
    }
    else {
      this.alertService.warningGrid();
    }
  }

  inativar() {
    let ids: string[] = this.gridOptions.api.getSelectedRows().map(item => item.Id);
    if (ids.length > 0) {
      this.appService.UsuarioInativar(ids)
        .subscribe(
          response => {
            this.alertService.success();
            this.grid();
          },
          error => this.errorService.commonCatch(error.status));
    }
    else {
      this.alertService.warningGrid();
    }
  }

}
