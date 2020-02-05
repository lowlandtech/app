import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-sidebar',
  template: '<ng-content></ng-content>'
})
export class SidebarComponent implements OnInit {
  @HostBinding('class.sidebar') class1 = true;
  
  constructor() { }

  ngOnInit() {
  }

}
