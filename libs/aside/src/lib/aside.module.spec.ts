import { async, TestBed } from '@angular/core/testing';
import { AsideModule } from './aside.module';

describe('AsideModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AsideModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(AsideModule).toBeDefined();
  });
});
