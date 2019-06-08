import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatPaginator, MatSort, MatDialogConfig } from '@angular/material';
import { User } from 'src/app/models/User';
import { AdminService } from 'src/app/services/admin.service';
import { AddPersonDialogComponent } from '../admin-panel/addPerson-dialog/addPerson-dialog.component';
import { DeleteDialogComponent } from '../admin-panel/delete-dialog/delete-dialog.component';
import { TeamManagment } from 'src/app/models/TeamManagment';
import { trigger, state, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'app-team-managment',
  templateUrl: './team-managment.component.html',
  styleUrls: ['./team-managment.component.css'],
})
export class TeamManagmentComponent implements OnInit {
  displayedColumns: string[] = ['id', 'team', 'leader', 'teamMates', 'Action'];
  teams: MatTableDataSource<TeamManagment>;
  searchKey: string;

  constructor(private adminService: AdminService, private dialog: MatDialog) {
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
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
  dialogConfig.disableClose = true;
  dialogConfig.autoFocus = true;
  dialogConfig.width = '50%';
  this.dialog.open(AddPersonDialogComponent, dialogConfig);
}

onCreateDeleteDialog(user: User) {
  const dialogConfig2 = new MatDialogConfig();
  dialogConfig2.disableClose = true;
  dialogConfig2.autoFocus = true;
  dialogConfig2.width = '50%';
  // user = this.users.data.find(p => p.id === user.id);
  // dialogConfig2.data = { id: user.id, userName : user.userName, fullUserName: user.fullUserName,
  //   team: user.team, roles: user.roles};
  // this.dialog.open(DeleteDialogComponent, dialogConfig2);
}
}
