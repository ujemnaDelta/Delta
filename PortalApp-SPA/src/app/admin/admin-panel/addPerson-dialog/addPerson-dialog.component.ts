import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { User } from 'src/app/models/User';
import { HttpClient } from '@angular/common/http';
import { AdminService } from 'src/app/services/admin.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { getTemplateContent } from '@angular/core/src/sanitization/html_sanitizer';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, Validators, FormControl } from '@angular/forms';

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
  myForm = this.fb.group({
    UserName: ['', [Validators.required, Validators.minLength(6)] ],
    FullUserName: ['', [Validators.required, Validators.minLength(6)] ],
    Position: ['', [Validators.required, Validators.minLength(6)]],
    team: ['', [Validators.required]],
    roles: ['', [Validators.required]],
    UserPassword: ['', [Validators.required,
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
});

  constructor( public dialogRef: MatDialogRef<AddPersonDialogComponent>, private adminService: AdminService,
    private alertify: AlertifyService, private authService: AuthService, private fb: FormBuilder) { }

  ngOnInit() {
    this.getTeams();
    this.getRoles();
    console.log(this.model);
    console.log(this.teams);
    console.log(this.teams);
  }

  register(form: FormControl) {
    console.log(form.value);
    this.authService.register(form.value).subscribe(() => {
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
