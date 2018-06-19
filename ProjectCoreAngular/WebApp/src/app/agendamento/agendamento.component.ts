import { Component, OnInit } from "@angular/core"
import { AppService } from "../app.service";
import { GridOptions } from "ag-grid";
import { NgForm } from "@angular/forms";
import { AlertService } from "../alert.service";
import { ErrorService } from "../error.service";
import { AgGridClickEditComponent } from "../ag-grid.click.edit.component";
import { HORA } from '../app.mask'

declare var $: any;

class ModelAgendamento {
  Id: number;
  Medico: string;
  Paciente: string;
  Data: Date;
  Hora: string;
}

@Component({
  selector: 'mt-agendamento',
  templateUrl: 'agendamento.component.html',
  styleUrls: ['agendamento.component.css'],
  providers: [ModelAgendamento]
})
export class AgendamentoComponent implements OnInit {

  public gridOptions: GridOptions;
  public rowData: any[];
  public columnDefs: any[];

  Mask: {
    Hora: Array<string | RegExp>;
  };

  constructor(private model: ModelAgendamento, private appService: AppService, private errorService: ErrorService, private alertService: AlertService) {

    this.Mask = {
      Hora: HORA
    }

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
      { headerName: "Médico", field: "Medico" },
      { headerName: "Paciente", field: "Paciente" },
      { headerName: "Data", field: "Data" },
      { headerName: "Hora", field: "Hora" },
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
    this.appService.AgendamentoGet(id)
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

    $(function () {
      $('#typeaheadmedico').typeahead({
        source: function (query, process) {
          $.ajax({
            url: 'http://localhost:55680/medico/suggestion',
            type: 'GET',
            data: 'filter=' + query,
            dataType: 'JSON',
            async: true,
            success: function (data) {
              process(data);
            }
          })
        }
      });
      $('#typeaheadpaciente').typeahead({
        source: function (query, process) {
          $.ajax({
            url: 'http://localhost:55680/paciente/suggestion',
            type: 'GET',
            data: 'filter=' + query,
            dataType: 'JSON',
            async: true,
            success: function (data) {
              process(data);
            }
          })
        }
      });
    });
  }

  grid() {
    this.appService.AgendamentoGrid()
      .subscribe(
        response => {
          this.rowData = response;
        },
        error => this.errorService.commonCatch(error.status));
  }

  save(myForm: NgForm) {
    this.model.Medico = $('#typeaheadmedico').val();
    this.model.Paciente = $('#typeaheadpaciente').val();
    if (myForm.form.valid) {
      if (!this.model.Id) {
        this.appService.AgendamentoPost(this.model)
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
        this.appService.AgendamentoPut(this.model.Id, this.model)
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

  clearDto() {
    this.model.Id = undefined;
    this.model.Medico = undefined;
    this.model.Paciente = undefined;
    this.model.Data = undefined;
    this.model.Hora = undefined;
  }

  excluir() {
    let ids: string[] = this.gridOptions.api.getSelectedRows().map(item => item.Id);
    if (ids.length > 0) {
      this.appService.AgendamentoDelete(ids)
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
