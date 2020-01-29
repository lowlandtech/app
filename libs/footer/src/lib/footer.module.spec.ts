import { async, TestBed } from '@angular/core/testing';
import { FooterModule } from './footer.module';

describe('FooterModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FooterModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(FooterModule).toBeDefined();
  });
});
