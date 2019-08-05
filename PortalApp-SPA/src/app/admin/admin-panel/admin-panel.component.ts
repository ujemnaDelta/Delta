import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  constructor(private alertify: AlertifyService) { }
  color = 'primary';
  value = 20;
  loading = true;
  ngOnInit() {
    setTimeout(() => {
      this.loading = false;
     }, 2000);
  }

}
