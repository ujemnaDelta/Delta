import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormBuilder, Validators } from '@angular/forms';
import { AdminService } from 'src/app/services/admin.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AddteamDialogComponent } from 'src/app/admin/admin-panel/addteam-dialog/addteam-dialog.component';
import { AlertifyService } from 'src/app/services/alertify.service';
import { LeaderService } from 'src/app/services/leader.service';
import { Opinion } from 'src/app/models/Opinion';
import { DatePipe, formatDate } from '@angular/common';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-AddOpinionDialog',
  templateUrl: './AddOpinionDialog.component.html',
  styleUrls: ['./AddOpinionDialog.component.css'],
  providers: [DatePipe]
})
export class AddOpinionDialogComponent implements OnInit {
  test: string;
  myDate = new Date();
  myForm = this.fb.group({
    evaluatedId: [{value : this.user.evaluatedId.valueOf(), disabled: true}, [Validators.required]],
    name: [{value : this.user.name.valueOf(), disabled: true}, [Validators.required]],
    position: [{value : this.user.position.valueOf(), disabled: true}, [Validators.required]],
    mainText: ['', [Validators.required]],
    leaderText: [''],
    leaderId: [this.user.leaderId, [Validators.required]],
    created: [{value : '', disabled: true}]
});
  constructor( private serviceLeader: LeaderService, private alertify: AlertifyService, @Inject(MAT_DIALOG_DATA) public user: Opinion,
               private fb: FormBuilder, public dialogRef: MatDialogRef<AddOpinionDialogComponent>, private datePipe: DatePipe) {
                this.myDate = new Date();
                this.test = this.datePipe.transform(this.myDate, 'dd.MM.yyyy');
                this.myForm.get('created').setValue(this.test);
                }
  ngOnInit() {
  }

  Add(form: FormControl) {
    form.value.evaluatedId = this.user.evaluatedId;
    form.value.name = this.user.name;
    form.value.created = this.test;
   this.serviceLeader.AddOpinion(form.value).subscribe(res => {
     this.onClose();
     this.alertify.success('Opinia została dodana pomyślnie');
     }, error => {
       this.alertify.error(error);
    });
  }
  onClose() {
    this.dialogRef.close();
}
}
