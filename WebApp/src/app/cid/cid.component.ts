import { Component, OnInit } from "@angular/core"
import { AppService } from "../app.service";

@Component({
  selector: 'mt-cid',
  templateUrl: 'cid.component.html',
  styleUrls: ['cid.component.css']
})
export class CidComponent implements OnInit {

  constructor(private appService: AppService) { }

  ngOnInit() {
  }

}
