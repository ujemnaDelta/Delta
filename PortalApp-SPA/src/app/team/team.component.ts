import { Component, OnInit } from '@angular/core';
import { MemberService } from '../services/member.service';
import { TeamLeaderPanel } from '../models/TeamLeaderPanel';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  teamMates: TeamLeaderPanel[];
  searchKey: string;
  UserId: number;

  constructor(private userService: MemberService, private authService: AuthService) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.UserId = this.authService.returnLeaderId();
    this.userService.getTeamMates(this.UserId).subscribe((team: TeamLeaderPanel[]) => {
      this.teamMates = team;
    }, error => {
      console.log(error);
    });
  }

}
