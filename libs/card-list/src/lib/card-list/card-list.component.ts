import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Observable } from 'rxjs';
import { CardData } from '@lowlandtech/api';
import { CardListConfig } from '../+state';
import { CardListFacade } from '../+state';

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

  drop(event: CdkDragDrop<any[]>) {
    this.cards$.subscribe(cards=>{
      moveItemInArray(cards, event.previousIndex, event.currentIndex);
    })
  }
}
