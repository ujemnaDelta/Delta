import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { CommonModule } from '@angular/common';
import {

  MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,

  MatToolbarModule, MatMenuModule, MatIconModule, MatProgressSpinnerModule

} from '@angular/material';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
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
      LeaderPanelComponent
   ],
   imports: [
      BrowserModule,
      CommonModule,
      MatToolbarModule,
      MatButtonModule,
      MatCardModule,
      MatInputModule,
      MatDialogModule,
      BrowserAnimationsModule,
      MatTableModule,
      MatMenuModule,
      MatIconModule,
      MatProgressSpinnerModule,
      HttpClientModule,
      FormsModule,
      MDBBootstrapModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
