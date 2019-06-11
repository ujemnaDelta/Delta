import { Component, OnInit, Inject } from '@angular/core';
import { OpinionWithoutLeaderText } from 'src/app/models/OpinionWithoutLeaderText';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-opinion-dialog',
  templateUrl: './opinion-dialog.component.html',
  styleUrls: ['./opinion-dialog.component.css']
})
export class OpinionDialogComponent implements OnInit {
  myForm = this.fb.group({
    leaderName: [{value : this.userOpinion.leaderName.valueOf(), disabled: true}],
    name: [{value : this.userOpinion.name.valueOf(), disabled: true}],
    created: [{value : this.userOpinion.created.valueOf(), disabled: true}],
    mainText: [{value : this.userOpinion.mainText.valueOf(), disabled: true}],
    position: [{value : this.userOpinion.position.valueOf(), disabled: true}],

});
  opinions: OpinionWithoutLeaderText;
  leader: any;
  constructor(private alertify: AlertifyService, @Inject(MAT_DIALOG_DATA) public userOpinion: OpinionWithoutLeaderText,
  public dialogRef: MatDialogRef<OpinionDialogComponent>, private fb: FormBuilder) {
    this.opinions = userOpinion;
  }

  ngOnInit() {
     console.log(this.opinions);
  }

  onClose() {
    this.dialogRef.close();
  }

}
