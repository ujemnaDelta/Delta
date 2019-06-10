import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatPaginator, MatSort, MatDialogConfig } from '@angular/material';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import { AddPersonDialogComponent } from '../admin-panel/addPerson-dialog/addPerson-dialog.component';
import { DeleteDialogComponent } from '../admin-panel/delete-dialog/delete-dialog.component';
import { TeamManagment } from 'src/app/models/TeamManagment';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { AddusertoteamDialogComponent } from '../admin-panel/addusertoteam-dialog/addusertoteam-dialog.component';
import { AddteamDialogComponent } from '../admin-panel/addteam-dialog/addteam-dialog.component';
import { DeleteteamDialogComponent } from '../admin-panel/deleteteam-dialog/deleteteam-dialog.component';

@Component({
  selector: 'app-team-managment',
  templateUrl: './team-managment.component.html',
  styleUrls: ['./team-managment.component.css'],
})
export class TeamManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'team', 'leaderName', 'teamMates', 'Action'];
  teams: MatTableDataSource<TeamManagment>;
  searchKey: string;

  constructor(private adminService: AdminService, private dialog: MatDialog) {
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
    this.adminService.getAllTeamManagment().subscribe((team: TeamManagment[]) => {
      this.teams = new MatTableDataSource(team);
      this.teams.paginator = this.paginator;
    this.teams.sort = this.sort;
    console.log(team);
    }, error => {
      console.log(error);
    });
  }
  applyFilter(filterValue: string) {
    this.teams.filter = filterValue.trim().toLowerCase();

    if (this.teams.paginator) {
      this.teams.paginator.firstPage();
    }
  }
  onSearchClear() {
    this.searchKey = '';
    this.applyFilter(this.searchKey);
}

onCreateDialog() {
  const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = false;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddusertoteamDialogComponent, dialogConfig);
}
onCreateAddTeamDialog() {
  const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = false;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddteamDialogComponent, dialogConfig);
}
onCreateDeleteDialog(user: TeamManagment) {
  const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = false;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  user = this.teams.data.find(p => p.id === user.id);
   dialogConfig.data = { id: user.id, leaderName : user.leaderName, team: user.team};
   this.dialog.open(DeleteteamDialogComponent, dialogConfig);
}
}
