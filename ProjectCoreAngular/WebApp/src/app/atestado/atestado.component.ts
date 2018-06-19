import { Component, OnInit } from "@angular/core"
import { AppService } from "../app.service";

@Component({
  selector: 'mt-atestado',
  templateUrl: 'atestado.component.html',
  styleUrls: ['atestado.component.css']
})
export class AtestadoComponent implements OnInit {

  constructor(private appService: AppService) { }

  ngOnInit() {
  }

}
