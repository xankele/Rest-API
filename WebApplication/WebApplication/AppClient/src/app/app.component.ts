import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'KittyApp';
  cats: any;
  users: any;
  adoptions: any;
  constructor(private http: HttpClient)
  {
    
  }
  ngOnInit()
  {
    this.getCats();
    this.getUsers();
    this.getAdoptions();
    
  }
  getCats() {
    this.http.get('https://localhost:44332/api/Cats').subscribe(response => {
      this.cats = response;
    }, error => {
      console.log(error);
    })
  }
  getUsers() {
    this.http.get('https://localhost:44332/api/Users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
  getAdoptions() {
    this.http.get('https://localhost:44332/api/Adoptions').subscribe(response => {
      this.adoptions = response;
    }, error => {
      console.log(error);
    })
  }
}