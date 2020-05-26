import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-sidebar-header',
  template: '<ng-content></ng-content>'
})
export class SidebarHeaderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
