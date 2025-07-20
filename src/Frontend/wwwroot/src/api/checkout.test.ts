import { createCheckoutRequest, submitCheckout } from './checkout';
import apiClient from './client';

// Mock the axios client to avoid actual API calls
jest.mock('./client', () => {
  return {
    __esModule: true,
    default: {
      post: jest.fn().mockResolvedValue({ 
        status: 200,
        data: {
          unitPrice: 50,
          totalAmount: 50,
          isBundleDiscounted: false,
          bundleQuantity: 1,
          itemId: '123',
          success: true
        }
      })
    }
  };
});

describe('Checkout API', () => {
  beforeEach(() => {
    // Clear all mocks before each test
    jest.clearAllMocks();
  });

  describe('createCheckoutRequest', () => {
    it('should create a valid checkout request with all fields', () => {
      const request = createCheckoutRequest(
        '123e4567-e89b-12d3-a456-426614174000',
        'PRODUCT-123',
        2,
        'piece',
        ['SUMMER10', 'WELCOME20']
      );

      expect(request).toEqual({
        userSessionId: '123e4567-e89b-12d3-a456-426614174000',
        sku: 'PRODUCT-123',
        quantity: 2,
        unitOfMeasure: 'piece',
        promoCodes: ['SUMMER10', 'WELCOME20']
      });
    });

    it('should create a valid checkout request without promo codes', () => {
      const request = createCheckoutRequest(
        '123e4567-e89b-12d3-a456-426614174000',
        'PRODUCT-123',
        2,
        'piece'
      );

      expect(request).toEqual({
        userSessionId: '123e4567-e89b-12d3-a456-426614174000',
        sku: 'PRODUCT-123',
        quantity: 2,
        unitOfMeasure: 'piece'
      });
    });

    it('should use "piece" as the default unit of measure when omitted', () => {
      const request = createCheckoutRequest(
        '123e4567-e89b-12d3-a456-426614174000',
        'PRODUCT-123',
        2
      );

      expect(request).toEqual({
        userSessionId: '123e4567-e89b-12d3-a456-426614174000',
        sku: 'PRODUCT-123',
        quantity: 2,
        unitOfMeasure: 'piece'
      });
    });
  });

  describe('submitCheckout', () => {
    it('should call the API with the correct endpoint and data', async () => {
      const checkoutData = createCheckoutRequest(
        '123e4567-e89b-12d3-a456-426614174000',
        'PRODUCT-123',
        2,
        'piece',
        ['SUMMER10']
      );

      await submitCheckout(checkoutData);

      // Access the mock through the default export
      expect(apiClient.post).toHaveBeenCalledWith('/api/v1/checkout', checkoutData);
      expect(apiClient.post).toHaveBeenCalledTimes(1);
    });

    it('should throw an error when the API call fails', async () => {
      // Mock the API to reject - access the mock through the default export
      (apiClient.post as jest.Mock).mockRejectedValueOnce(new Error('API Error'));

      const checkoutData = createCheckoutRequest(
        '123e4567-e89b-12d3-a456-426614174000',
        'PRODUCT-123',
        2,
        'piece'
      );

      await expect(submitCheckout(checkoutData)).rejects.toThrow('API Error');
    });
  });
});