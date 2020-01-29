import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-header',
  template: `
    <header class="app-header navbar">
      <button class="navbar-toggler d-lg-none" type="button" scxMobileSidebarToggler>
        <span class="navbar-toggler-icon"></span>
      </button>
      <a class="navbar-brand" href="#"></a>
      <button class="navbar-toggler d-md-down-none mr-auto" type="button" scxSidebarToggler>
        <span class="navbar-toggler-icon"></span>
      </button>
      <button class="navbar-toggler d-md-down-none" type="button" scxAsideMenuToggler>
        <span class="navbar-toggler-icon"></span>
      </button>
    </header>
  `,
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
