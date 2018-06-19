import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule, ModuleWithComponentFactories } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular/main';
import { ROUTES } from './app.route';
import { AppComponent } from './app.component'
import { LoginComponent } from './login/login.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { MedicoComponent } from './medico/medico.component';
import { PacienteComponent } from './paciente/paciente.component';
import { AgendamentoComponent } from './agendamento/agendamento.component';
import { AuthGuard } from './Authentication/auth.guard';
import { AuthenticationService } from './authentication/authentication.service';
import { AppService } from './app.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './Authentication/jwt.interceptor';
import { ErrorService } from './error.service';
import { ShowErrorsComponent } from './error.component';
import { AlertComponent } from './alert/alert.component';
import { AlertService } from './alert.service';
import { CidComponent } from './cid/cid.component';
import { AtestadoComponent } from './atestado/atestado.component';
import { AgGridClickEditComponent } from './ag-grid.click.edit.component';
import { TextMaskModule } from 'angular2-text-mask';
import { ModalComponent } from './modal/modal.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    UsuarioComponent,
    MedicoComponent,
    PacienteComponent,
    AgendamentoComponent,
    CidComponent,
    AtestadoComponent,
    ShowErrorsComponent,
    AlertComponent,
    AgGridClickEditComponent,
    ModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule,
    HttpClientModule,
    RouterModule.forRoot(ROUTES),
    AgGridModule.withComponents([AgGridClickEditComponent]),
    TextMaskModule
  ],
  providers: [
    AuthGuard,
    AuthenticationService,
    AppService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    ErrorService,
    AlertService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
