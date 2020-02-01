import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { ArticleData } from '@spotacard/api';

@Component({
  selector: 'app-card-list-item',
  templateUrl: './card-list-item.component.html',
  styleUrls: ['./card-list-item.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ArticleListItemComponent {
  @Input() card: ArticleData;
  @Output() favorite: EventEmitter<string> = new EventEmitter();
  @Output() unFavorite: EventEmitter<string> = new EventEmitter();
  @Output() navigateToArticle: EventEmitter<string> = new EventEmitter();

  toggleFavorite(card: ArticleData) {
    if (card.favorited) {
      this.unFavorite.emit(card.slug);
    } else {
      this.favorite.emit(card.slug);
    }
  }
}
