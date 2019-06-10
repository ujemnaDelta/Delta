import { Component, OnInit } from '@angular/core';
import { ShowOpinion } from '../models/ShowOpinion';
import { LeaderService } from '../services/leader.service';
import { AlertifyService } from '../services/alertify.service';
import { MemberService } from '../services/member.service';
import { OpinionWithoutLeaderText } from '../models/OpinionWithoutLeaderText';
import { AuthService } from '../services/auth.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { OpinionDialogComponent } from './opinion-dialog/opinion-dialog.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  opinions: OpinionWithoutLeaderText[];
  leader: any;
  UserId: number;
  constructor(private serviceUser: MemberService, private alertify: AlertifyService, private authService: AuthService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.getAll();
  }

  // getLeaderName() {
  //   this.serviceLeader.GetLeader(this.userOpinion.leaderId).subscribe((user: any) => {
  //     this.leader = user;
  //   }, error => {
  //     console.log(error);
  //   });
  // }
  getAll() {
    this.UserId = this.authService.returnLeaderId();
    this.serviceUser.GetUserOpinion(this.UserId).subscribe((user: OpinionWithoutLeaderText[]) => {
      this.opinions = user;
      console.log(this.opinions);
      this.alertify.success('Pomyślnie wczytano opinię');
    }, error => {
      console.log(error);
    });
  }

  AddOpinionDialog(user: OpinionWithoutLeaderText) {

    const dialogConfig2 = new MatDialogConfig();
    dialogConfig2.disableClose = false;
    dialogConfig2.autoFocus = true;
    dialogConfig2.width = '70%';
    dialogConfig2.height = '67%';
    user = this.opinions.find( p => p.mainText === user.mainText);
     dialogConfig2.data = { evaluatedId: user.evaluatedId , position: user.position, name: user.name,
      leaderName: user.leaderName, created: user.created, mainText: user.mainText};
      this.dialog.open(OpinionDialogComponent, dialogConfig2);
  }
}
