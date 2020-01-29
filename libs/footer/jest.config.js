module.exports = {
  name: 'footer',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/footer',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
