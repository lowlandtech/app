import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy
} from '@angular/core';
import { CardData, CardStatus } from '@spotacard/api';

@Component({
  selector: 'scx-card-list-item',
  templateUrl: './card-list-item.component.html',
  styleUrls: ['./card-list-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardListItemComponent {
  @Input() card: CardData;
  @Output() favorite: EventEmitter<string> = new EventEmitter();
  @Output() unFavorite: EventEmitter<string> = new EventEmitter();
  @Output() navigateToCard: EventEmitter<string> = new EventEmitter();
  @Output() statusChange: EventEmitter<CardStatus> = new EventEmitter();

  get isCollapsed(){
    return this.card.status === CardStatus.MINIMIZED;
  }

  toggleFavorite(card: CardData) {
    if (card.favorited) {
      this.unFavorite.emit(card.slug);
    } else {
      this.favorite.emit(card.slug);
    }
  }

  onCollapse() {
    if(this.card.status !== CardStatus.MINIMIZED){
      this.card.status = CardStatus.MINIMIZED;
    } else if(this.card.status === CardStatus.MINIMIZED){
      this.card.status = CardStatus.NORMAL;
    }
    this.statusChange.emit(this.card.status)
  }

  showNote(card: CardData) { }
}
