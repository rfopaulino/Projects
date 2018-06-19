import { Component, OnInit } from "@angular/core"
import { UsuarioComponent } from "../usuario/usuario.component";

@Component({
  selector: 'mt-modal',
  templateUrl: 'modal.component.html',
  providers: [UsuarioComponent]
})
export class ModalComponent implements OnInit {

  constructor() {
  }

  ngOnInit() {
  }

}
