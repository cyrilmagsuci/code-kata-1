import apiClient from './client';

// Types based on the API requirements
export interface CheckoutItemRequest {
  userSessionId: string; // UUID format
  sku: string;
  quantity: number;
  unitOfMeasure: string;
  promoCodes?: string[];
}

// Response type from the API
export interface CheckoutResponse {
  unitPrice: number;
  totalAmount: number;
  isBundleDiscounted: boolean;
  bundleQuantity: number;
  itemId: string;
  success: boolean;
}

export const submitCheckout = async (checkoutData: CheckoutItemRequest): Promise<CheckoutResponse> => {
  try {
    const response = await apiClient.post('/api/v1/checkout', checkoutData);
    return response.data as CheckoutResponse;
  } catch (error) {
    console.error('Error submitting checkout:', error);
    throw error;
  }
};

export const createCheckoutRequest = (
  userSessionId: string,
  skuValue: string,
  quantityValue: number,
  unitOfMeasureValue: string = "piece",
  promoCodeValues?: string[]
): CheckoutItemRequest => {
  const request: CheckoutItemRequest = {
    userSessionId,
    sku: skuValue,
    quantity: quantityValue,
    unitOfMeasure: unitOfMeasureValue,
  };

  if (promoCodeValues && promoCodeValues.length > 0) {
    request.promoCodes = promoCodeValues;
  }

  return request;
};