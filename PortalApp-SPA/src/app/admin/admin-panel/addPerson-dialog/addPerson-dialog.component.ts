import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { User } from 'src/app/models/User';
import { HttpClient } from '@angular/common/http';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { getTemplateContent } from '@angular/core/src/sanitization/html_sanitizer';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-addPerson-dialog',
  templateUrl: './addPerson-dialog.component.html',
  styleUrls: ['./addPerson-dialog.component.css']
})
export class AddPersonDialogComponent implements OnInit {
  model: any = {};
  teams: any;
  roles: any;
  constructor( public dialogRef: MatDialogRef<AddPersonDialogComponent>, private adminService: AdminService,
    private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.getTeams();
    this.getRoles();

  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('registration successful');
      this.onClose();
    }, error => {
      this.alertify.error(error);
    });
}

  getTeams() {
    this.adminService.getTeam().subscribe(response => {
      this.teams = response;
    }, error => {
      this.alertify.error('Failed load teams');
    });
  }

  getRoles() {
    this.adminService.getRoles().subscribe(response => {
      this.roles = response;
    }, error => {
      this.alertify.error('Failed load roles');
    });
  }
  onClose() {
    this.dialogRef.close();
  }
}
