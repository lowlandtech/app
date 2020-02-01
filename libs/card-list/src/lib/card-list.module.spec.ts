import { async, TestBed } from '@angular/core/testing';
import { CardListModule } from './card-list.module';

describe('CardListModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [CardListModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(CardListModule).toBeDefined();
  });
});
