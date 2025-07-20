import type { Config } from 'jest';

const config: Config = {
  // Indicates whether the coverage information should be collected while executing the test
  collectCoverage: true,
  
  // The directory where Jest should output its coverage files
  coverageDirectory: "coverage",
  
  // A list of paths to directories that Jest should use to search for files in
  roots: ["<rootDir>/src"],
  
  // The test environment that will be used for testing
  testEnvironment: "jsdom",
  
  // The glob patterns Jest uses to detect test files
  testMatch: ["**/__tests__/**/*.ts?(x)", "**/?(*.)+(spec|test).ts?(x)"],
  
  // An array of regexp pattern strings that are matched against all test paths, matched tests are skipped
  testPathIgnorePatterns: ["/node_modules/"],
  
  // A map from regular expressions to paths to transformers
  transform: {
    "^.+\\.(ts|tsx)$": "ts-jest"
  },
  
  // An array of regexp pattern strings that are matched against all source file paths, matched files will skip transformation
  transformIgnorePatterns: ["/node_modules/"],
  
  // Indicates whether each individual test should be reported during the run
  verbose: true,
  
  // Setup files after environment is initialized
  // setupFilesAfterEnv: ['<rootDir>/jest.setup.ts'],
  
  // Module file extensions for importing
  moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx', 'json', 'node'],
  
  // Module name mapper for handling module aliases
  moduleNameMapper: {
    // Add any module name mappings here if needed
    // For example, if you're using path aliases in tsconfig.json:
    // "^@/(.*)$": "<rootDir>/src/$1"
  },
  
  // Handle ES modules
  extensionsToTreatAsEsm: ['.ts', '.tsx'],
  
  // Configure ts-jest for ES modules
  preset: 'ts-jest/presets/js-with-ts-esm',
  
  // Set the test environment to node for API tests
  // testEnvironment: 'node',
  
  // Configure ts-jest
  globals: {
    'ts-jest': {
      useESM: true,
    },
  },
};

export default config;