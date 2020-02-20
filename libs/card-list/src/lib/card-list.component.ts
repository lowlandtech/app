import { CardData } from '@spotacard/api';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { CardListFacade } from './+state/card-list.facade';
import { CardListConfig } from './+state/card-list.reducer';

@Component({
  selector: 'scx-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardListComponent implements OnInit {
  totalPages$: Observable<number[]>;
  cards$: Observable<CardData[]>;
  listConfig$: Observable<CardListConfig>;
  isLoading$: Observable<boolean>;

  constructor(private facade: CardListFacade) {}

  ngOnInit() {
    this.totalPages$ = this.facade.totalPages$;
    this.cards$ = this.facade.cards$;
    this.listConfig$ = this.facade.listConfig$;
    this.isLoading$ = this.facade.isLoading$;
  }

  favorite(slug: string) {
    this.facade.favorite(slug);
  }

  unFavorite(slug: string) {
    this.facade.unFavorite(slug);
  }

  navigateToCard(slug: string) {
    this.facade.navigateToCard(slug);
  }

  setPage(page: number) {
    this.facade.setPage(page);
  }
}
