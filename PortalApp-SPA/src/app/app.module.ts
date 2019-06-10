import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { JwtModule } from '@auth0/angular-jwt';
import { CommonModule } from '@angular/common';
import {

  MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,

  MatToolbarModule, MatMenuModule, MatIconModule, MatProgressSpinnerModule, MatPaginatorModule,
  MatSortModule, MatCheckboxModule, MatDatepickerModule, MatGridListModule, MatRadioModule,
   MatNativeDateModule, MatOptionModule, MatSelectModule,

} from '@angular/material';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule, FormGroupDirective } from '@angular/forms';
import { UserComponent } from './user/user.component';
import {AuthService} from 'src/app/services/auth.service';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { ErrorInterceptorProvider } from './services/error.interceptor';
import { AlertifyService } from './services/alertify.service';
import { HomeComponent } from './home/home.component';
import { MainComponent } from './main/main.component';
import { TeamComponent } from './team/team.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HrPanelComponent } from './hr/hr-panel/hr-panel.component';
import { LeaderPanelComponent } from './leader/leader-panel/leader-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { UserManagmentComponent } from './admin/user-managment/user-managment.component';
import { TeamManagmentComponent } from './admin/team-managment/team-managment.component';
import {MatTabsModule} from '@angular/material/tabs';
import { AdminService } from './services/admin.service';
import { AddPersonDialogComponent } from './admin/admin-panel/addPerson-dialog/addPerson-dialog.component';
import { DeleteDialogComponent } from './admin/admin-panel/delete-dialog/delete-dialog.component';
import { AddusertoteamDialogComponent } from './admin/admin-panel/addusertoteam-dialog/addusertoteam-dialog.component';
import { AddteamDialogComponent } from './admin/admin-panel/addteam-dialog/addteam-dialog.component';
import { DeleteteamDialogComponent } from './admin/admin-panel/deleteteam-dialog/deleteteam-dialog.component';
import { LeaderService } from './services/leader.service';
import { LeaderTeamManagmentComponent } from './leader/LeaderTeamManagment/LeaderTeamManagment.component';
import { AddOpinionDialogComponent } from './leader/dialogs/AddOpinionDialog/AddOpinionDialog.component';
import { ShowopinionsDialogComponent } from './leader/dialogs/showopinions-dialog/showopinions-dialog.component';
import {MatExpansionModule} from '@angular/material/expansion';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      UserComponent,
      HomeComponent,
      MainComponent,
      TeamComponent,
      AdminPanelComponent,
      HrPanelComponent,
      LeaderPanelComponent,
      HasRoleDirective,
      UserManagmentComponent,
      TeamManagmentComponent,
      AddPersonDialogComponent,
      DeleteDialogComponent,
      AddusertoteamDialogComponent,
      AddteamDialogComponent,
      DeleteteamDialogComponent,
      LeaderTeamManagmentComponent,
      AddOpinionDialogComponent,
      ShowopinionsDialogComponent

   ],
   imports: [
      BrowserModule,
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatToolbarModule,
      MatTabsModule,
      MatRadioModule,
      MatButtonModule,
      MatOptionModule,
      MatSelectModule,
      MatCardModule,
      MatNativeDateModule,
      MatInputModule,
      MatDialogModule,
      BrowserAnimationsModule,
      MatTableModule,
      MatMenuModule,
      MatIconModule,
      MatExpansionModule,
      MatDialogModule,
      MatProgressSpinnerModule,
      HttpClientModule,
      MatDatepickerModule,
      MatCheckboxModule,
      MatGridListModule,
      FormsModule,
      MatExpansionModule,
      MatPaginatorModule,
      MatSortModule,
      MDBBootstrapModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          whitelistedDomains: ['localhost:5000'],
          blacklistedRoutes: ['localhost:5000/api/authorization']
        }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      AdminService,
      LeaderService
   ],
   bootstrap: [
      AppComponent
   ],
   entryComponents: [AddPersonDialogComponent, DeleteDialogComponent,
    AddusertoteamDialogComponent, AddteamDialogComponent,
    DeleteteamDialogComponent, AddOpinionDialogComponent,
    ShowopinionsDialogComponent]
})
export class AppModule { }
