import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {
  title: string = 'Happy Warehouse';

  constructor(private titleService: Title) {
    this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
  }

}
