import { Component } from '@angular/core';

@Component({
  template: `
    <scx-header></scx-header>
    <scx-content>
      <scx-sidebar>
        <scx-sidebar-header></scx-sidebar-header>
        <scx-sidebar-form></scx-sidebar-form>
        <scx-sidebar-nav></scx-sidebar-nav>
        <scx-sidebar-footer></scx-sidebar-footer>
        <button class="sidebar-minimizer" type="button" scxSidebarMinimizer scxBrandMinimizer></button>
      </scx-sidebar>
      <scx-main>
        <scx-breadcrumbs></scx-breadcrumbs>
        <div class="container-fluid">
          <router-outlet></router-outlet>
        </div>
      </scx-main>
      <scx-aside></scx-aside>
    </scx-content>
    <csx-footer>
      <span><a href="https://spotacard.com">Spotacard</a> &copy; 2020</span>
      <span class="ml-auto">Powered by <a href="https://lowlandtech.com">LowLandTech</a></span>
    </csx-footer>
  `
})
export class AdminComponent { }
