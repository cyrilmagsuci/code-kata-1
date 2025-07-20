/**
 * SimpleCheckout component provides a basic UI for sending checkout item information to the backend
 */
import React, { useState } from 'react';
import { submitCheckout, createCheckoutRequest } from '../api/checkout';
import { products } from '../utils/products';

// Props interface
interface SimpleCheckoutProps {
  onItemScanned?: (itemId: string, sku: string, quantity: number, totalAmount: number, unitPrice: number, isBundleDiscounted: boolean, bundleQuantity: number) => void;
  sharedUserSessionId?: string;
  onUserSessionIdChange?: (userSessionId: string) => void;
}

// Styles for the component
const styles = {
  container: {
    maxWidth: '500px',
    margin: '0 auto',
    padding: '2rem',
    border: '1px solid #ddd',
    borderRadius: '8px',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
  },
  title: {
    fontSize: '1.5rem',
    fontWeight: 'bold',
    marginBottom: '1.5rem',
    textAlign: 'center' as const,
  },
  formGroup: {
    marginBottom: '1.5rem',
  },
  label: {
    display: 'block',
    marginBottom: '0.5rem',
    fontWeight: 'bold',
  },
  select: {
    width: '100%',
    padding: '0.75rem',
    borderRadius: '4px',
    border: '1px solid #ddd',
    fontSize: '1rem',
  },
  input: {
    width: '100%',
    padding: '0.75rem',
    borderRadius: '4px',
    border: '1px solid #ddd',
    fontSize: '1rem',
  },
  button: {
    width: '100%',
    padding: '0.75rem',
    backgroundColor: '#4CAF50',
    color: 'white',
    border: 'none',
    borderRadius: '4px',
    fontSize: '1rem',
    cursor: 'pointer',
    transition: 'background-color 0.3s',
  },
  buttonDisabled: {
    opacity: 0.7,
    cursor: 'not-allowed',
  },
  message: {
    marginTop: '1rem',
    padding: '0.75rem',
    borderRadius: '4px',
    textAlign: 'center' as const,
  },
  error: {
    backgroundColor: '#ffebee',
    color: '#f44336',
  },
  success: {
    backgroundColor: '#e8f5e9',
    color: '#4CAF50',
  },
};

const SimpleCheckout: React.FC<SimpleCheckoutProps> = ({ 
  onItemScanned, 
  sharedUserSessionId = '', 
  onUserSessionIdChange 
}) => {
  // Form state
  const [sku, setSku] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [unitOfMeasure] = useState('piece'); // Fixed as 'piece' per requirements
  const [localUserSessionId, setLocalUserSessionId] = useState('');
  
  // Use shared userSessionId if provided, otherwise use local state
  const userSessionId = sharedUserSessionId || localUserSessionId;
  const setUserSessionId = (id: string) => {
    if (onUserSessionIdChange) {
      onUserSessionIdChange(id);
    } else {
      setLocalUserSessionId(id);
    }
  };
  
  // UI state
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  const [totalAmount, setTotalAmount] = useState<number | null>(null);
  const [unitPrice, setUnitPrice] = useState<number | null>(null);
  const [isBundleDiscounted, setIsBundleDiscounted] = useState(false);
  const [bundleQuantity, setBundleQuantity] = useState(1);
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Reset states
    setIsLoading(true);
    setError(null);
    setSuccess(false);
    setTotalAmount(null);
    setUnitPrice(null);
    setIsBundleDiscounted(false);
    setBundleQuantity(1);
    
    try {
      // Validate userSessionId
      if (!userSessionId.trim()) {
        throw new Error('User Session ID is required');
      }
      
      // Create and submit the checkout request
      const response = await submitCheckout(
        createCheckoutRequest(
          userSessionId,
          sku,
          quantity,
          unitOfMeasure
        )
      );
      
      // Show success message and update state with response data
      setSuccess(true);
      setTotalAmount(response.totalAmount);
      setUnitPrice(response.unitPrice);
      setIsBundleDiscounted(response.isBundleDiscounted);
      setBundleQuantity(response.bundleQuantity);
      
      // Call the onItemScanned handler if provided
      if (onItemScanned) {
        onItemScanned(
          response.itemId, 
          sku, 
          quantity, 
          response.totalAmount, 
          response.unitPrice,
          response.isBundleDiscounted,
          response.bundleQuantity
        );
      }
      
      // Reset form fields except userSessionId
      setSku('');
      setQuantity(1);
    } catch (err) {
      // Show error message
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError('Failed to complete checkout. Please try again.');
      }
      console.error('Checkout error:', err);
    } finally {
      setIsLoading(false);
    }
  };
  
  return (
    <div style={styles.container}>
      <h2 style={styles.title}>Simple Checkout</h2>
      
      <form onSubmit={handleSubmit}>
        <div style={styles.formGroup}>
          <label htmlFor="userSessionId" style={styles.label}>User Session ID</label>
          <input
            id="userSessionId"
            type="text"
            value={userSessionId}
            onChange={(e) => setUserSessionId(e.target.value)}
            style={styles.input}
            placeholder="Enter a session ID or generate one"
            required
          />
          <div style={{ marginTop: '0.5rem', display: 'flex', justifyContent: 'flex-end' }}>
            <button 
              type="button" 
              onClick={() => setUserSessionId(crypto.randomUUID())}
              style={{ 
                padding: '0.25rem 0.5rem', 
                fontSize: '0.8rem',
                backgroundColor: '#f0f0f0',
                border: '1px solid #ddd',
                borderRadius: '4px',
                cursor: 'pointer'
              }}
            >
              Generate UUID
            </button>
          </div>
        </div>
        
        <div style={styles.formGroup}>
          <label htmlFor="sku" style={styles.label}>Product SKU</label>
          <select
            id="sku"
            value={sku}
            onChange={(e) => setSku(e.target.value)}
            style={styles.select}
            required
          >
            <option value="">Select a product</option>
            {products.map(product => (
              <option key={product.sku} value={product.sku}>
                {product.name} (SKU: {product.sku})
              </option>
            ))}
          </select>
        </div>
        
        <div style={styles.formGroup}>
          <label htmlFor="quantity" style={styles.label}>Quantity</label>
          <input
            id="quantity"
            type="number"
            min="1"
            value={quantity}
            onChange={(e) => setQuantity(parseInt(e.target.value, 10))}
            style={styles.input}
            required
          />
        </div>
        
        <div style={styles.formGroup}>
          <label htmlFor="unitOfMeasure" style={styles.label}>Unit of Measure</label>
          <div 
            style={{
              ...styles.input, 
              backgroundColor: '#e8f5e9', 
              color: '#2e7d32',
              fontWeight: 'bold',
              display: 'flex',
              alignItems: 'center',
              border: '1px solid #4CAF50'
            }}
          >
            {unitOfMeasure}
          </div>
        </div>
        
        <button
          type="submit"
          style={{
            ...styles.button,
            ...(isLoading ? styles.buttonDisabled : {})
          }}
          disabled={isLoading || !sku}
        >
          {isLoading ? 'Processing...' : 'Submit Checkout'}
        </button>
      </form>
      
      {error && (
        <div style={{...styles.message, ...styles.error}}>
          {error}
        </div>
      )}
      
      {success && (
        <div style={{...styles.message, ...styles.success}}>
          Checkout completed successfully!
          {unitPrice !== null && unitPrice !== undefined && (
            <div style={{ marginTop: '0.5rem', fontWeight: 'bold' }}>
              Unit Price: ${unitPrice.toFixed(2)}
            </div>
          )}
          {isBundleDiscounted && totalAmount !== null && totalAmount !== undefined && (
            <div style={{ marginTop: '0.5rem', fontWeight: 'bold' }}>
              Bundle Price: ${totalAmount.toFixed(2)}
            </div>
          )}
          {isBundleDiscounted && bundleQuantity > 1 && (
            <div style={{ 
              marginTop: '0.5rem', 
              fontWeight: 'bold',
              color: '#2e7d32',
              backgroundColor: '#e8f5e9',
              padding: '0.5rem',
              borderRadius: '4px',
              border: '1px solid #4CAF50'
            }}>
              Bundle Discount Applied! Quantity in bundle: {bundleQuantity}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default SimpleCheckout;