import { async, TestBed } from '@angular/core/testing';
import { PlaygroundModule } from './playground.module';

describe('PlaygroundModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [PlaygroundModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(PlaygroundModule).toBeDefined();
  });
});
