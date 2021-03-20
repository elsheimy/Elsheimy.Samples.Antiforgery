import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  private balance: number = 0;

  constructor(private api: ApiService) {

  }
  ngOnInit(): void {
    this.api.balance().subscribe(val => this.balance = val);
    }

  


  debit() {
    this.api.debit(10).subscribe(val => this.balance = val);
  }

  credit() {
    this.api.credit(10).subscribe(val => this.balance = val);
  }
}
