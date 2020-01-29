import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-sidebar',
  template: '<ng-content></ng-content>'
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
