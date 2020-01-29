import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-sidebar-nav',
  template: '<ng-content></ng-content>'
})
export class SidebarNavComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
