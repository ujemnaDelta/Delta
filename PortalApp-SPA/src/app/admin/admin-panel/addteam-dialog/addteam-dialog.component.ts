import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormControl } from '@angular/forms';
import { AdminService } from 'src/app/services/admin.service';
import { MatDialogRef } from '@angular/material';
import { AlertifyService } from 'src/app/services/alertify.service';
@Component({
  selector: 'app-addteam-dialog',
  templateUrl: './addteam-dialog.component.html',
  styleUrls: ['./addteam-dialog.component.css']
})
export class AddteamDialogComponent implements OnInit {
  myForm = this.fb.group({
    TeamName: ['', [Validators.required]],
});
  constructor( private serviceAdmin: AdminService, private alertify: AlertifyService,
               private fb: FormBuilder, public dialogRef: MatDialogRef<AddteamDialogComponent>) { }
  ngOnInit() {
  }

  Add(form: FormControl) {

    this.serviceAdmin.addTeam(form.value).subscribe(res => {
      this.onClose();
    }, error => {
      this.alertify.error(error);
  });
  }
  onClose() {
    this.dialogRef.close();
}

}
