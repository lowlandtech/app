import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-sidebar-minimizer',
  template: '<button class="sidebar-minimizer" type="button" scxSidebarMinimizer scxBrandMinimizer></button>'
})
export class SidebarMinimizerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
