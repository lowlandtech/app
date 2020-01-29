import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-content',
  template: `
    <ng-content></ng-content>
})
export class ContentComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
