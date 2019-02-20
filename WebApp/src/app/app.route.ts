import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { PacienteComponent } from "./paciente/paciente.component";
import { UsuarioComponent } from "./usuario/usuario.component";
import { MedicoComponent } from "./medico/medico.component";
import { AgendamentoComponent } from "./agendamento/agendamento.component";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./Authentication/auth.guard";
import { CidComponent } from "./cid/cid.component";
import { AtestadoComponent } from "./atestado/atestado.component";

export const ROUTES: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'usuario', component: UsuarioComponent },
  { path: 'medico', component: MedicoComponent },
  { path: 'paciente', component: PacienteComponent },
  { path: 'agendamento', component: AgendamentoComponent },
  { path: 'cid', component: CidComponent },
  { path: 'atestado', component: AtestadoComponent }
];
