module.exports = {
  name: 'ngrx-error',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/ngrx-error',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
