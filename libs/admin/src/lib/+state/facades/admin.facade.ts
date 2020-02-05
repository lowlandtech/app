import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AdminState as AdminState } from '../reducers';
import { adminQuery } from '../selectors';
import {
  SidebarHidden,
  SidebarMaximized,
  SidebarMinimized,
  ProfileHidden,
  ProfileShown,
  AsideShown,
  AsideHidden
} from '../actions';

@Injectable()
export class AdminStateFacade {
	public sidebar$ = this.store.select(adminQuery.getSidebar);
	public profile$ = this.store.select(adminQuery.getProfile);
	public aside$ = this.store.select(adminQuery.getAside);
  public admin$ = this.store.select(adminQuery.getAdmin);
  public logo$ = this.store.select(adminQuery.getLogo);

	constructor(private store: Store<AdminState>) { }

  hideProfile() {
		this.store.dispatch(new ProfileHidden);
  }

  showProfile() {
		this.store.dispatch(new ProfileShown);
  }

	hide(state: boolean) {
    if(state){
      this.store.dispatch(new SidebarHidden);
    } else {
      this.store.dispatch(new SidebarMaximized);
    }
	}

	minimize() {
		this.store.dispatch(new SidebarMinimized);
	}

	maximize() {
		this.store.dispatch(new SidebarMaximized);
  }

  asideHide() {
    this.store.dispatch(new AsideHidden);
  }

  asideShow() {
    this.store.dispatch(new AsideShown);
  }
}
