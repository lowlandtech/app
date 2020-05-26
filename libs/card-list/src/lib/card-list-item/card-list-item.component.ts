import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy
} from '@angular/core';
import { CardData } from '@lowlandtech/api';

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

  toggleFavorite(card: CardData) {
    if (card.favorited) {
      this.unFavorite.emit(card.slug);
    } else {
      this.favorite.emit(card.slug);
    }
  }

  showNote(card: CardData) { }
}
