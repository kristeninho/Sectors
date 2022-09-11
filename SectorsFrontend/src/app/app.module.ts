import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'
import { ReactiveFormsModule } from '@angular/forms';

import { SectorsFormComponent } from './components/sectors-form/sectors-form.component';
import { SectorService } from './services/sector.service';

const routes: Routes = [
  {
    path: '', component: SectorsFormComponent
  }
];

export function resolveBeforeAppStarts(sectorService: SectorService) {
  return () => sectorService.getSectorDataForInitialization().toPromise();
}

@NgModule({
  declarations: [
    SectorsFormComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports:[
    [RouterModule]
  ],
  providers: [{
    provide: APP_INITIALIZER,
    useFactory: resolveBeforeAppStarts,
    deps: [SectorService],
    multi: true
  }],
  bootstrap: [SectorsFormComponent]
})
export class AppModule { }
