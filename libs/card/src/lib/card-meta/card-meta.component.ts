import {
  Component,
  Input,
  ChangeDetectionStrategy,
  EventEmitter,
  Output
} from '@angular/core';
import { CardData } from '@lowlandtech/api';

@Component({
  selector: 'scx-card-meta',
  templateUrl: './card-meta.component.html',
  styleUrls: ['./card-meta.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardMetaComponent {
  @Input() card: CardData;
  @Input() isAuthenticated: boolean;
  @Input() canModify: boolean;
  @Output() follow: EventEmitter<string> = new EventEmitter<string>();
  @Output() unfollow: EventEmitter<string> = new EventEmitter<string>();
  @Output() unfavorite: EventEmitter<string> = new EventEmitter();
  @Output() favorite: EventEmitter<string> = new EventEmitter();
  @Output() delete: EventEmitter<string> = new EventEmitter();

  toggleFavorite() {
    if (this.card.favorited) {
      this.unfavorite.emit(this.card.slug);
    } else {
      this.favorite.emit(this.card.slug);
    }
  }

  toggleFollow() {
    if (this.card.author.following) {
      this.unfollow.emit(this.card.author.username);
    } else {
      this.follow.emit(this.card.author.username);
    }
  }

  deleteCard() {
    this.delete.emit(this.card.slug);
  }
}
