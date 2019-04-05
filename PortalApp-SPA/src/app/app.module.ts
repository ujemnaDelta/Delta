import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {

  MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,

  MatToolbarModule, MatMenuModule, MatIconModule, MatProgressSpinnerModule

} from '@angular/material';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent
   ],
   imports: [
      BrowserModule,
      CommonModule,
      MatToolbarModule,
      MatButtonModule,
      MatCardModule,
      MatInputModule,
      MatDialogModule,
      MatTableModule,
      MatMenuModule,
      MatIconModule,
      MatProgressSpinnerModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
