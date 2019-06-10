import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-leader-panel',
  templateUrl: './leader-panel.component.html',
  styleUrls: ['./leader-panel.component.css']
})
export class LeaderPanelComponent implements OnInit {

  LeaderId: number;
  constructor() { }

  ngOnInit() {
  }

}
