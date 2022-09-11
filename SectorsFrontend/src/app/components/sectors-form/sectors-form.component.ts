import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ISector } from 'src/app/interfaces/ISector';
import { IUser } from 'src/app/interfaces/IUser';
import { SectorService } from 'src/app/services/sector.service';

@Component({
  selector: 'app-sectors-form',
  templateUrl: './sectors-form.component.html',
  styleUrls: ['./sectors-form.component.scss']
})
export class SectorsFormComponent{

  sectorsForm = this.formBuilder.group({
    name: ["", [Validators.required, Validators.maxLength(30), Validators.minLength(3), Validators.pattern("^[a-zA-Z '.-]*$")]],
    formSectors: [null, Validators.required],
    agreeToTerms: [false, [Validators.required, Validators.requiredTrue]]
  });

  sectors: ISector[] = this.sectorService.initializedSectors;
  selectedOptions: number[] = [];

  constructor(private sectorService: SectorService, private formBuilder: FormBuilder) { }

  onSubmit(){
    let user: IUser = {
      name: this.sectorsForm.get('name')?.value ?? "",
      agreedToTerms: this.sectorsForm.get('agreeToTerms')?.value ?? true,
      sectorIds: this.sectorsForm.get('formSectors')?.value ?? []
    };
    
    this.sectorService.addOrUpdateUser(user).subscribe((response: any)=> {},
      (error)=>{alert(error.error.text)}
    );
  }

  loadDataForUser(){
    this.sectorService.getUserData(this.sectorsForm.get('name')?.value ?? "")
      .subscribe((response: IUser) =>{
        this.selectedOptions = response.sectorIds;
        this.sectorsForm.get('agreeToTerms')?.setValue(response.agreedToTerms);
      },
      (error)=>alert(error.error))
  }
}
