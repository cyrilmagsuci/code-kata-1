/**
 * CheckoutHistory component displays the history of scanned items
 */
import React from 'react';
import type { Product } from '../types/checkout';

// Define the interface for a scanned item
interface ScannedItem {
  id: string;
  sku: string;
  quantity: number;
  timestamp: Date;
  product: Product;
  totalAmount: number | undefined | null;
  unitPrice: number | undefined | null;
  isBundleDiscounted: boolean;
  bundleQuantity: number;
}

// Props interface
interface CheckoutHistoryProps {
  items: ScannedItem[];
  grandTotal?: number | null;
}

const styles = {
  container: {
    maxWidth: '500px',
    margin: '2rem auto',
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
  emptyMessage: {
    textAlign: 'center' as const,
    color: '#666',
    padding: '1rem',
  },
  itemList: {
    listStyle: 'none',
    padding: 0,
    margin: 0,
  },
  item: {
    padding: '1rem',
    borderBottom: '1px solid #eee',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  itemDetails: {
    flex: 1,
  },
  itemName: {
    fontWeight: 'bold',
    marginBottom: '0.25rem',
  },
  itemMeta: {
    fontSize: '0.9rem',
    color: '#666',
  },
  itemPrice: {
    fontWeight: 'bold',
    color: '#4CAF50',
  },
  totalContainer: {
    marginTop: '1.5rem',
    padding: '1rem',
    backgroundColor: '#f9f9f9',
    borderRadius: '4px',
    textAlign: 'right' as const,
  },
  totalLabel: {
    fontWeight: 'bold',
    marginRight: '0.5rem',
  },
  totalValue: {
    fontWeight: 'bold',
    color: '#4CAF50',
    fontSize: '1.2rem',
  },
};

const CheckoutHistory: React.FC<CheckoutHistoryProps> = ({ items, grandTotal }) => {

  const displayGrandTotal = grandTotal !== undefined && grandTotal !== null ? grandTotal : 0;
  
  // Format date for display
  const formatDate = (date: Date): string => {
    return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  };
  
  return (
    <div style={styles.container}>
      <h2 style={styles.title}>Checkout History</h2>
      
      {items.length === 0 ? (
        <div style={styles.emptyMessage}>
          No items have been scanned yet.
        </div>
      ) : (
        <>
          <ul style={styles.itemList}>
            {items.map((item) => (
              <li key={item.id} style={styles.item}>
                <div style={styles.itemDetails}>
                  <div style={styles.itemName}>
                    {item.product.name} x {item.quantity}
                  </div>
                  <div style={styles.itemMeta}>
                    SKU: {item.sku} | Scanned at: {formatDate(item.timestamp)}
                  </div>
                </div>
                <div style={styles.itemPrice}>
                  <div>Unit Price: ${item.unitPrice !== undefined && item.unitPrice !== null ? item.unitPrice.toFixed(2) : '0.00'}</div>
                  {item.isBundleDiscounted && (
                    <div>Bundle Price: ${item.totalAmount !== undefined && item.totalAmount !== null ? item.totalAmount.toFixed(2) : '0.00'}</div>
                  )}
                  {item.isBundleDiscounted && item.bundleQuantity > 1 && (
                    <div style={{ 
                      marginTop: '0.25rem',
                      fontSize: '0.85rem',
                      fontWeight: 'bold',
                      color: '#2e7d32',
                      backgroundColor: '#e8f5e9',
                      padding: '0.25rem 0.5rem',
                      borderRadius: '4px',
                      border: '1px solid #4CAF50'
                    }}>
                      Bundle: {item.bundleQuantity}
                    </div>
                  )}
                </div>
              </li>
            ))}
          </ul>
          
          <div style={styles.totalContainer}>
            <span style={styles.totalLabel}>Grand Total:</span>
            <span style={styles.totalValue}>${displayGrandTotal.toFixed(2)}</span>
          </div>
        </>
      )}
    </div>
  );
};

export default CheckoutHistory;