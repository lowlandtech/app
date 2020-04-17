import { Observable, Subject } from 'rxjs';
import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  OnDestroy,
  Injector
} from '@angular/core';

import { takeUntil } from 'rxjs/operators';
import { CardListConfig } from '@lowlandtech/card-list';
import { cardListInitialState, CardListFacade } from '@lowlandtech/card-list';
import { AuthFacade } from '@lowlandtech/auth';
import { HomeFacade } from './+state/home.facade';
import { AdminStateFacade } from '@lowlandtech/admin';
import { TagsListComponent } from './tags-list/tags-list.component';

@Component({
  selector: 'scx-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit, OnDestroy {
  listConfig$: Observable<CardListConfig>;
  tags$: Observable<string[]>;
  isAuthenticated: boolean;
  unsubscribe$: Subject<void> = new Subject();

  constructor(
    private facade: HomeFacade,
    private cardListFacade: CardListFacade,
    private authFacade: AuthFacade,
    private adminFacade: AdminStateFacade
  ) {}

  public ngOnInit(): void {
    this.authFacade.isLoggedIn$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(isLoggedIn => {
        this.isAuthenticated = isLoggedIn;
        this.getCards();
      });
    this.facade.selectedTag$.subscribe(tag => {
      this.setListTag(tag);
    });
    this.listConfig$ = this.cardListFacade.listConfig$;
    this.tags$ = this.facade.tags$;
    this.adminFacade.addAsideListGroup({
      title: 'Popular Tags',
      component: TagsListComponent,
      isOpen: true,
      data: {
        injector: Injector.create([
          { provide: HomeFacade, useValue: this.facade }
        ])
      }
    });
  }

  setListTo(type: string = 'ALL') {
    this.cardListFacade.setListConfig(<CardListConfig>{
      ...cardListInitialState.listConfig,
      type
    });
  }

  getCards() {
    if (this.isAuthenticated) {
      this.setListTo('FEED');
    } else {
      this.setListTo('ALL');
    }
  }

  setListTag(tag: string) {
    this.cardListFacade.setListConfig(<CardListConfig>{
      ...cardListInitialState.listConfig,
      filters: {
        ...cardListInitialState.listConfig.filters,
        tag
      }
    });
  }

  ngOnDestroy() {
    this.adminFacade.removeAsideListGroup('Popular Tags');
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
