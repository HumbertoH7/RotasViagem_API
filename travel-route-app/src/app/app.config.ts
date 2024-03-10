import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { RouteFormComponent } from './route-form/route-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { RouteSearchComponent } from './route-search/route-search.component';


const routes: Routes = [
  { path: '', redirectTo: '/form', pathMatch: 'full' },
  { path: 'form', component: RouteFormComponent },

];

@NgModule({
  declarations: [
    AppComponent,
    RouteFormComponent,
    RouteSearchComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule, // Adicione o ReactiveFormsModule aos imports do módulo
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppRoutingModule { }
export class AppModule { }

export const appConfig = {
  apiUrl: 'http://localhost:3000/api',
  providers: []
};





