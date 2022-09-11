import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ISector } from '../interfaces/ISector';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class SectorService {

  constructor(private http: HttpClient) { }

  initializedSectors: ISector[] = [];

  private baseUrl: string = "http://localhost:4200/api/";
  private dataInitialisationEndpoint: string = "Sectors/GetAllSectorsSeperatedByCategories";
  private addOrUpdateUserEndpoint: string = "Users/AddOrUpdateUser";
  private getUserDataEndpoint: string = "Users/GetUserData";

  //GET REQUESTS
  getSectorDataForInitialization(){
    return this.http.get<ISector[]>(this.baseUrl + this.dataInitialisationEndpoint).pipe(
      tap((x: ISector[]) => (this.initializedSectors = x)
    ));
  }

  getUserData(userName: string): Observable<IUser> {
    return this.http.get<IUser>(this.baseUrl + this.getUserDataEndpoint , { params: new HttpParams().set("userName", userName) });
  }

  //POST REQUESTS
  addOrUpdateUser(user: IUser): Observable<IUser>{
    return this.http.post<IUser>(this.baseUrl + this.addOrUpdateUserEndpoint, user);
  }
}
