import { Component, OnInit, ViewChild } from '@angular/core';
import { TeamManagment } from 'src/app/models/TeamManagment';
import { MatDialogConfig, MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { AuthService } from 'src/app/services/auth.service';
import { LeaderService } from 'src/app/services/leader.service';
import { TeamLeaderPanel } from 'src/app/models/TeamLeaderPanel';
import { AddOpinionDialogComponent } from '../dialogs/AddOpinionDialog/AddOpinionDialog.component';
import { FormControl } from '@angular/forms';
import { ShowopinionsDialogComponent } from '../dialogs/showopinions-dialog/showopinions-dialog.component';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-LeaderTeamManagment',
  templateUrl: './LeaderTeamManagment.component.html',
  styleUrls: ['./LeaderTeamManagment.component.css']
})
export class LeaderTeamManagmentComponent implements OnInit {

  displayedColumns: string[] = ['id', 'PersonName', 'PersonPosition', 'Action'];
  teamMates: MatTableDataSource<TeamLeaderPanel>;
  searchKey: string;
  LeaderId: number;
  constructor(private leaderService: LeaderService, private dialog: MatDialog, private authService: AuthService) {
    dialog.afterAllClosed
    .subscribe(() => {
    // update a variable or call a function when the dialog closes
      this.getAll();
    }
  );
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.LeaderId = this.authService.returnLeaderId();
    this.leaderService.getUsersWithRoles(this.LeaderId).subscribe((team: TeamLeaderPanel[]) => {
      this.teamMates = new MatTableDataSource(team);
       this.teamMates.paginator = this.paginator;
     this.teamMates.sort = this.sort;
    }, error => {
      console.log(error);
    });
  }
  applyFilter(filterValue: string) {
    this.teamMates.filter = filterValue.trim().toLowerCase();

    if (this.teamMates.paginator) {
      this.teamMates.paginator.firstPage();
    }
  }
  onSearchClear() {
    this.searchKey = '';
    this.applyFilter(this.searchKey);
}

OpenOpinions(user: TeamLeaderPanel) {
  console.log(user);
  const dialogConfig2 = new MatDialogConfig();
  dialogConfig2.disableClose = false;
  dialogConfig2.autoFocus = true;
  dialogConfig2.width = '80%';
  dialogConfig2.height = '80%';

  dialogConfig2.data = { evaluatedId: user.id, leaderId: this.LeaderId, name: user.name};
  this.dialog.open(ShowopinionsDialogComponent, dialogConfig2);
}

AddOpinionDialog(user: TeamLeaderPanel) {
  const dialogConfig2 = new MatDialogConfig();
  dialogConfig2.disableClose = false;
  dialogConfig2.autoFocus = true;
  dialogConfig2.width = '80%';
  dialogConfig2.height = '80%';
  user = this.teamMates.data.find(p => p.id === user.id);
   dialogConfig2.data = { evaluatedId: user.id, position: user.position, name: user.name, leaderId: this.LeaderId};
    this.dialog.open(AddOpinionDialogComponent, dialogConfig2);
}

}
