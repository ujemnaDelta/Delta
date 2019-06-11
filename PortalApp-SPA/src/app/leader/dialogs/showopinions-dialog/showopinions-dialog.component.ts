import { Component, OnInit, Inject } from '@angular/core';
import { LeaderService } from 'src/app/services/leader.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Opinion } from 'src/app/models/Opinion';
import { AddOpinionDialogComponent } from '../AddOpinionDialog/AddOpinionDialog.component';
import { FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { User } from 'src/app/models/User';
import * as moment from 'moment';
import { ShowOpinion } from 'src/app/models/ShowOpinion';

@Component({
  selector: 'app-showopinions-dialog',
  templateUrl: './showopinions-dialog.component.html',
  styleUrls: ['./showopinions-dialog.component.css']
})
export class ShowopinionsDialogComponent implements OnInit {
  panelOpenState = false;
  opinions: ShowOpinion[];
  leader: any;
  constructor(private serviceLeader: LeaderService, private alertify: AlertifyService, @Inject(MAT_DIALOG_DATA) public userOpinion: Opinion,
  private fb: FormBuilder, public dialogRef: MatDialogRef<ShowopinionsDialogComponent>) {
   }

  ngOnInit() {
    this.getLeaderName();
    this.getAll();
  }

  onClose() {
    this.dialogRef.close();
  }
  getLeaderName() {
    this.serviceLeader.GetLeader(this.userOpinion.leaderId).subscribe((user: any) => {
      this.leader = user;

    }, error => {
      this.alertify.error(error);
    });
  }
  getAll() {
    this.serviceLeader.GetUserOpinion(this.userOpinion.evaluatedId).subscribe((user: ShowOpinion[]) => {
      this.opinions = user;
    }, error => {
      this.alertify.error(error);
    });
  }
}
